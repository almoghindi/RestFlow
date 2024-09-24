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
      { name: "IngredientIds", label: "Ingredients" },
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

export type SectionNames =
  | "Categories"
  | "Dishes"
  | "Ingredients"
  | "Waiters"
  | "Tables";

export const sectionIdFields: Record<string, string> = {
  Categories: "categoryId",
  Dishes: "dishId",
  Ingredients: "ingredientId",
  Waiters: "waiterId",
  Tables: "tableId",
};

export const sectionToLabel: Record<SectionNames, string> = {
  Categories: "category",
  Dishes: "dish",
  Ingredients: "ingredient",
  Waiters: "waiter",
  Tables: "table",
};
