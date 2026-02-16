interface SchemaField {
    name: string;
    type: "string" | "number" | "boolean";
    required: boolean;
}

interface CategorySchema {
    [categoryId: number]: SchemaField[];
}

const schemas: CategorySchema = {
    1: [
        { name: "Cores", type: "number", required: true },
        { name: "Threads", type: "number", required: true },
        { name: "BaseClock", type: "string", required: true },
        { name: "BoostClock", type: "string", required: true },
        { name: "Socket", type: "string", required: true },
        { name: "Manufacturer", type: "string", required: true }
    ],
    2: [
        { name: "VRAM", type: "string", required: true },
        { name: "BoostClock", type: "string", required: true },
        { name: "PCIe", type: "string", required: true },
        { name: "Brand", type: "string", required: true },
        { name: "Manufacturer", type: "string", required: true }
    ],
    3: [
        { name: "Socket", type: "string", required: true },
        { name: "FormFactor", type: "string", required: true },
        { name: "Chipset", type: "string", required: true }
    ],
    4: [
        { name: "Capacity", type: "string", required: true },
        { name: "Type", type: "string", required: true },
        { name: "Speed", type: "string", required: true }
    ],
    5: [
        { name: "Capacity", type: "string", required: true },
        { name: "FormFactor", type: "string", required: true },
        { name: "Interface", type: "string", required: true }
    ],
    6: [
        { name: "Wattage", type: "string", required: true },
        { name: "Efficiency", type: "string", required: true },
        { name: "Modular", type: "string", required: true }
    ],
    7: [
        { name: "Type", type: "string", required: true },
        { name: "Fans", type: "number", required: true },
        { name: "RadiatorSize", type: "string", required: false }
    ],
    8: [
        { name: "FormFactor", type: "string", required: true },
        { name: "Color", type: "string", required: true }
    ],
    9: [
        { name: "Type", type: "string", required: true },
        { name: "DPI", type: "string", required: false },
        { name: "Buttons", type: "number", required: false },
        { name: "Connectivity", type: "string", required: true },
        { name: "RGB", type: "string", required: true },
        { name: "Keys", type: "number", required: false }
    ],
    10: [
        { name: "Size", type: "string", required: true },
        { name: "Resolution", type: "string", required: true },
        { name: "Panel", type: "string", required: true },
        { name: "RefreshRate", type: "string", required: true }
    ]
};

document.addEventListener("DOMContentLoaded", () => {
    const categorySelect = document.querySelector<HTMLInputElement | HTMLSelectElement>("#Product_CategoryId");
    const specsInput = document.querySelector<HTMLInputElement>("#Product_SpecsJson")!;
    const form = document.querySelector<HTMLFormElement>("form[method='post']");
    if (!categorySelect || !specsInput || !form) return;

    const descriptionField = document.querySelector<HTMLTextAreaElement>("textarea[name='Product.Description']");
    const specsContainer = document.createElement("div");
    specsContainer.id = "specs-container";
    specsContainer.style.marginTop = "1rem";

    if (descriptionField) {
        descriptionField.insertAdjacentElement("afterend", specsContainer);
    } else {
        categorySelect.insertAdjacentElement("afterend", specsContainer);
    }

    const existingSpecs: Record<string, string> = specsInput.dataset.specs ? JSON.parse(specsInput.dataset.specs) : {};

    function generateSpecsForm(categoryId: number) {
        specsContainer.innerHTML = "";
        const schema = schemas[categoryId];
        if (!schema) return;

        for (const field of schema) {
            const wrapper = document.createElement("div");
            wrapper.className = "mb-3";

            if (specsContainer.children.length === 0) wrapper.classList.add("mt-3");

            const label = document.createElement("label");
            label.className = "form-label";
            label.classList.add("stat-label");
            label.textContent = field.name + (field.required ? " *" : "");
            wrapper.appendChild(label);

            const input = document.createElement("input");
            input.type = "text";
            input.className = "form-control";
            input.classList.add("neon-input");
            input.dataset.specField = field.name;
            input.value = existingSpecs[field.name] ?? "";

            const errorSpan = document.createElement("span");
            errorSpan.className = "text-danger d-block";
            errorSpan.classList.add("text-neon-red");

            input.addEventListener("input", () => {
                validateSingleField(input, field, errorSpan);
                syncJson();
            });

            input.addEventListener("blur", () => {
                validateSingleField(input, field, errorSpan);
            });

            wrapper.appendChild(input);
            wrapper.appendChild(errorSpan);
            specsContainer.appendChild(wrapper);
        }

        syncJson();
    }

    function validateSingleField(input: HTMLInputElement, field: SchemaField, errorSpan: HTMLSpanElement): boolean {
        const value = input.value.trim();

        if (field.required && !value) {
            errorSpan.textContent = `${field.name} is required.`;
            input.classList.add("is-invalid");
            input.classList.remove("is-valid");
            return false;
        }

        if (field.type === "number" && value && isNaN(Number(value))) {
            errorSpan.textContent = "Please enter a valid number.";
            input.classList.add("is-invalid");
            input.classList.remove("is-valid");
            return false;
        }

        errorSpan.textContent = "";
        input.classList.remove("is-invalid");
        if (value) input.classList.add("is-valid");
        return true;
    }

    function collectSpecs(): Record<string, string> {
        const specs: Record<string, string> = {};
        specsContainer.querySelectorAll<HTMLInputElement>("input[data-spec-field]").forEach(input => {
            specs[input.dataset.specField!] = input.value.trim();
        });
        return specs;
    }

    function validateSpecs(schema: SchemaField[]): boolean {
        let valid = true;
        specsContainer.querySelectorAll<HTMLInputElement>("input[data-spec-field]").forEach(input => {
            const field = schema.find(f => f.name === input.dataset.specField)!;
            const errorSpan = input.nextElementSibling as HTMLSpanElement;
            if (!validateSingleField(input, field, errorSpan)) valid = false;
        });

        if (!valid) {
            const firstErrorInput = specsContainer.querySelector<HTMLInputElement>(".is-invalid");
            firstErrorInput?.scrollIntoView({ behavior: "smooth", block: "center" });
        }

        return valid;
    }

    function syncJson() {
        specsInput.value = JSON.stringify(collectSpecs());
    }

    if (categorySelect instanceof HTMLSelectElement) {
        categorySelect.addEventListener("change", () => {
            generateSpecsForm(Number(categorySelect.value));
        });
    }

    form.addEventListener("submit", (e) => {
        const categoryId = Number(categorySelect.value);
        const schema = schemas[categoryId];
        if (!schema) return;

        if (!validateSpecs(schema)) {
            e.preventDefault();
            e.stopPropagation();
            return;
        }

        syncJson(); 
    });

    generateSpecsForm(Number(categorySelect.value));
    syncJson();
});


