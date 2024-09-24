import React from "react";
import DishCard from "./dish-card";

interface DishesSectionProps {
  dishes: any[];
  formatIngredients: (ingredients: { name: string }[]) => string;
}

const DishesSection: React.FC<DishesSectionProps> = ({
  dishes,
  formatIngredients,
}) => {
  return (
    <div
      className="grid gap-4 pr-4"
      style={{
        gridTemplateColumns: "repeat(auto-fit, minmax(250px, 1fr))",
      }}
    >
      {dishes.length > 0 ? (
        dishes.map((dish, index) => (
          <DishCard
            key={dish.id || index}
            dish={dish}
            formatIngredients={formatIngredients}
          />
        ))
      ) : (
        <p className="text-gray-400">
          No dishes found for the selected category.
        </p>
      )}
    </div>
  );
};

export default DishesSection;
