export interface SectionField {
  name: string;
  label: string;
  required?: boolean;
}

export interface SectionConfig {
  name: string;
  fields: SectionField[];
}

export const sectionsConfig: SectionConfig[] = [
  {
    name: "Categories",
    fields: [
      { name: "name", label: "Name", required: true },
      { name: "description", label: "Description" },
    ],
  },
  {
    name: "Dishes",
    fields: [
      { name: "name", label: "Name", required: true },
      { name: "price", label: "Price", required: true },
      { name: "CategoryId", label: "Category", required: true },
      { name: "Ingredients", label: "Ingredients", required: true },
      { name: "imageUrl", label: "Image URL" },
      { name: "isAvailable", label: "Is Available" },
    ],
  },
  {
    name: "Ingredients",
    fields: [
      { name: "name", label: "Name", required: true },
      { name: "pricePerUnit", label: "Price", required: true },
      { name: "quantity", label: "Quantity", required: true },
      { name: "description", label: "Description" },
      { name: "isAvailable", label: "Is Available" },
    ],
  },
  {
    name: "Waiters",
    fields: [
      { name: "fullName", label: "Name", required: true },
      { name: "password", label: "Password", required: true },
      { name: "contactInformation", label: "Contact Information" },
    ],
  },
  {
    name: "Tables",
    fields: [
      { name: "tableNumber", label: "Number", required: true },
      { name: "capacity", label: "Capacity", required: true },
      { name: "isAvailable", label: "Is Available" },
    ],
  },
];
