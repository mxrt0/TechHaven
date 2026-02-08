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


const categorySelect = document.querySelector<HTMLSelectElement>("#Product_CategoryId")!;
const specsContainer = document.createElement("div");
specsContainer.id = "specs-container";
specsContainer.style.marginTop = "1rem";
categorySelect.insertAdjacentElement("afterend", specsContainer);

function generateSpecsForm(categoryId: number) {
    specsContainer.innerHTML = "";
    const schema = schemas[categoryId];
    if (!schema) return;

    for (const field of schema) {
        const wrapper = document.createElement("div");
        wrapper.className = "mb-3";

        if (specsContainer.children.length === 0) {
            wrapper.classList.add("mt-3");
        }

        const label = document.createElement("label");
        label.className = "form-label";
        label.textContent = field.name + (field.required ? " *" : "");
        wrapper.appendChild(label);

        const input = document.createElement("input");
        input.type = "text";
        input.className = "form-control";
        input.name = `spec_${field.name}`;
        input.dataset.specField = field.name;
        

        input.addEventListener("input", () => {
            syncJson();
            validateSingleField(input, field, errorSpan);
        });
        input.addEventListener("blur", () => {
            validateSingleField(input, field, errorSpan);
        });
        const errorSpan = document.createElement("span");
        errorSpan.className = "text-danger d-block";

        wrapper.appendChild(input);
        wrapper.appendChild(errorSpan);
        specsContainer.appendChild(wrapper);
    }
}
function validateSingleField(input: HTMLInputElement, field: SchemaField, errorSpan: HTMLSpanElement) {
    const value = input.value.trim();
    let isRequired = field.required;

    if (isRequired && !value) {
        errorSpan.textContent = `${field.name} is required.`;
        input.classList.add("is-invalid");
        return false;
    }

    if (field.type === "number" && value && isNaN(Number(value))) {
        errorSpan.textContent = "Please enter a valid number.";
        input.classList.add("is-invalid");
        return false;
    }

    errorSpan.textContent = "";
    input.classList.remove("is-invalid");
        input.classList.add("is-valid"); 
    return true;
}

function collectSpecs(): Record<string, string> {
    const inputs = specsContainer.querySelectorAll<HTMLInputElement>("input[data-spec-field]");
    const specs: Record<string, string> = {};
    inputs.forEach(input => {
        specs[input.dataset.specField!] = input.value;
    });
    return specs;
}

function validateSpecs(schema: SchemaField[]): boolean {
    let valid = true;
    const inputs = specsContainer.querySelectorAll<HTMLInputElement>("input[data-spec-field]");

    inputs.forEach(input => {
        const fieldName = input.dataset.specField!;
        const schemaField = schema.find(f => f.name === fieldName)!;
        const errorSpan = input.nextElementSibling as HTMLSpanElement;

        if (schemaField?.required && !input.value.trim() || validateSingleField(input, schemaField, errorSpan)) {
            errorSpan.textContent = "This field is required.";
            input.classList.add("is-invalid");
            valid = false;
        } else {
            errorSpan.textContent = "";
            input.classList.remove("is-invalid");
        }
    });
    if (!valid) {
        const firstErrorInput = specsContainer.querySelector<HTMLInputElement>(".is-invalid");
        firstErrorInput?.scrollIntoView({ behavior: "smooth", block: "center" });
    }
    return valid;
}

function syncJson() {
    const specsInput = document.getElementById("Product_SpecsJson") as HTMLInputElement;
    if (specsInput) {
        specsInput.value = JSON.stringify(collectSpecs());
    }
}

categorySelect.addEventListener("change", () => {
    generateSpecsForm(Number(categorySelect.value));
});

const form = document.querySelector<HTMLFormElement>("form[method='post']")!;
form.addEventListener("submit", (e) => {
    const categoryId = Number(categorySelect.value);
    const schema = schemas[categoryId]!;

    if (!validateSpecs(schema)) {
        e.preventDefault();
        e.stopPropagation();
        const firstError = specsContainer.querySelector(".is-invalid");
        firstError?.scrollIntoView({ behavior: "smooth", block: "center" });
        return;
    }

    const specsInput = document.getElementById("Product_SpecsJson") as HTMLInputElement;
    specsInput.value = JSON.stringify(collectSpecs());
}, true);

document.addEventListener("DOMContentLoaded", () => {
    generateSpecsForm(Number(categorySelect.value));
});
