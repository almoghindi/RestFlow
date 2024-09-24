import React from "react";

interface DishCardProps {
  dish: any;
  formatIngredients: (ingredients: { name: string }[]) => string;
}

const DishCard: React.FC<DishCardProps> = ({ dish, formatIngredients }) => {
  return (
    <div
      key={dish.id}
      className="relative rounded-lg shadow-md overflow-hidden flex flex-col group"
      style={{
        width: "100%",
        paddingTop: "56.25%",
        position: "relative",
        flexShrink: 0,
      }}
    >
      <img
        src={dish.imageUrl}
        alt={dish.name}
        className="absolute top-0 left-0 w-full h-full object-cover transition-opacity duration-300 group-hover:opacity-50"
        style={{ borderRadius: "8px" }}
      />

      <div
        className="absolute top-2 left-2 bg-black bg-opacity-70 text-white rounded px-2 py-1 z-10"
        style={{
          display: "inline-block",
        }}
      >
        <h3 className="font-semibold text-lg">{dish.name}</h3>
      </div>

      <div
        className="absolute inset-0 bg-black bg-opacity-70 flex justify-center items-center opacity-0 transition-opacity duration-300 group-hover:opacity-100"
        style={{
          borderRadius: "8px",
        }}
      >
        <p className="text-white text-md text-center">
          {formatIngredients(dish.ingredients)}
        </p>
      </div>

      <div
        className="absolute top-2 right-2 bg-black bg-opacity-70 text-white rounded px-2 py-1"
        style={{
          display: "inline-block",
        }}
      >
        <span className="font-bold">{dish.price} ₪</span>
      </div>
    </div>
  );
};

export default DishCard;
