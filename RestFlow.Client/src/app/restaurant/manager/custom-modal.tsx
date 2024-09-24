import React, { useEffect } from "react";
import { Modal } from "@/components/ui/modal";
import { Input } from "@/components/ui/input";
import { Checkbox } from "@/components/ui/checkbox";
import { Dropdown } from "@/components/ui/dropdown";
import { MultiSelectDropdown } from "@/components/ui/multi-select-dropdown";

interface CustomModalProps {
  title: string;
  isOpen: boolean;
  onClose: () => void;
  onSave: () => void;
  saveButtonText: string;
  fields: Array<{ name: string; label: string }>;
  currentItem: any;
  setCurrentItem: (item: any) => void;
  categories?: any[];
  ingredients?: any[];
}

const CustomModal: React.FC<CustomModalProps> = ({
  title,
  isOpen,
  onClose,
  onSave,
  saveButtonText,
  fields,
  currentItem,
  setCurrentItem,
  categories = [],
  ingredients = [],
}) => {
  const handleMultiSelectChange = (selectedIds: string[]) => {
    setCurrentItem({
      ...currentItem,
      ingredientIds: selectedIds,
    });
  };

  const getIngredientsValuesList = (ingredientIds: string[] = []) => {
    return ingredientIds;
  };

  return (
    <Modal
      title={title}
      isOpen={isOpen}
      onClose={onClose}
      onSave={onSave}
      saveButtonText={saveButtonText}
    >
      <div className="space-y-4">
        {fields.map((field) => (
          <div key={field.name} className="flex items-center">
            <label className="w-1/3 text-sm font-medium text-gray-700">
              {field.label}
            </label>
            {field.name === "CategoryId" ? (
              <Dropdown
                items={categories.map((cat) => ({
                  value: cat.categoryId,
                  name: cat.name,
                }))}
                value={currentItem?.categoryId || ""}
                onChange={(value) => {
                  const selectedCategory = categories.find(
                    (cat) => cat.categoryId === value
                  );
                  setCurrentItem({
                    ...currentItem,
                    categoryId: value,
                    category: selectedCategory ? selectedCategory.name : "",
                  });
                }}
              />
            ) : field.name === "isAvailable" ? (
              <Checkbox
                checked={!!currentItem && !!currentItem[field.name]}
                onCheckedChange={(checked) =>
                  setCurrentItem({
                    ...currentItem,
                    [field.name]: checked,
                  })
                }
              />
            ) : field.name === "IngredientIds" ? (
              <MultiSelectDropdown
                options={ingredients.map((ingredient) => ({
                  value: ingredient.ingredientId,
                  label: ingredient.name,
                }))}
                selectedValues={getIngredientsValuesList(
                  currentItem?.ingredientIds
                )}
                onChange={handleMultiSelectChange}
              />
            ) : (
              <Input
                type="text"
                value={currentItem?.[field.name] || ""}
                onChange={(e) =>
                  setCurrentItem({
                    ...currentItem,
                    [field.name]: e.target.value,
                  })
                }
              />
            )}
          </div>
        ))}
      </div>
    </Modal>
  );
};

export default CustomModal;
