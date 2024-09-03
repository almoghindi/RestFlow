import React from "react";
import { Button } from "@/components/ui/button";
import { PlusCircle, Pencil, Trash2 } from "lucide-react";
import { CheckCircle, XCircle } from "lucide-react";

interface DashboardSectionProps {
  title: string;
  items: any[];
  fields: { name: string; label: string; required?: boolean }[];
  onAdd: () => void;
  onEdit: (item: any) => void;
  onDelete: (item: any) => void;
  categories: { id: string; name: string }[];
}

const DashboardSection: React.FC<DashboardSectionProps> = ({
  title,
  items,
  fields,
  onAdd,
  onEdit,
  onDelete,
  categories,
}) => {
  const getCategoryName = (categoryId: string) => {
    const category = categories.find((cat) => cat.id === categoryId);
    return category ? category.name : "Unknown";
  };

  const columnCount = fields.length + 1;
  const columnWidth = `w-[calc(100% / ${columnCount})]`;

  return (
    <div className="bg-white rounded-lg shadow-md p-6">
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-semibold text-gray-800">{title}</h2>
        <Button
          onClick={onAdd}
          className="flex items-center gap-2"
          variant="success"
        >
          <PlusCircle size={18} />
          Add {title}
        </Button>
      </div>

      <div className="overflow-x-auto">
        <table className="w-full border-collapse">
          <thead>
            <tr className="bg-gray-100">
              {fields.map((field) => (
                <th
                  key={field.name}
                  className={`px-4 py-3 text-left text-sm font-semibold text-gray-600 uppercase tracking-wider ${columnWidth}`}
                >
                  {field.label}
                </th>
              ))}
              <th className={`px-4 py-3 ${columnWidth}`}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item, index) => (
              <tr
                key={item.id}
                className={`hover:bg-gray-50 transition-colors ${
                  index % 2 === 0 ? "bg-white" : "bg-gray-50"
                }`}
              >
                {fields.map((field) => (
                  <td
                    key={field.name}
                    className={`px-4 py-3 text-gray-800 ${columnWidth}`}
                  >
                    {field.name === "CategoryId" ? (
                      getCategoryName(item[field.name])
                    ) : field.name === "isAvailable" ? (
                      item[field.name] ? (
                        <CheckCircle className="text-green-500" size={18} />
                      ) : (
                        <XCircle className="text-red-500" size={18} />
                      )
                    ) : (
                      item[field.name]
                    )}
                  </td>
                ))}
                <td className={`px-4 py-3 ${columnWidth}`}>
                  <div className="flex justify-center gap-2">
                    <Button
                      onClick={() => onEdit(item)}
                      className="p-2 text-blue-600 hover:text-blue-800"
                      variant="ghost"
                    >
                      <Pencil size={18} />
                    </Button>
                    <Button
                      onClick={() => onDelete(item)}
                      className="p-2 text-red-600 hover:text-red-800"
                      variant="ghost"
                    >
                      <Trash2 size={18} />
                    </Button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default DashboardSection;
