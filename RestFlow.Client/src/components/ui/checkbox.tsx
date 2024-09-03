import * as React from "react";
import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import { Tick01Icon } from "hugeicons-react";

interface CheckboxProps {
  checked: boolean;
  onCheckedChange: (checked: boolean) => void;
  label?: string;
}

export const Checkbox: React.FC<CheckboxProps> = ({
  checked,
  onCheckedChange,
  label,
}) => (
  <div className="flex items-center space-x-2">
    <CheckboxPrimitive.Root
      className="w-6 h-6 bg-white border-2 border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 mb-1"
      checked={checked}
      onCheckedChange={onCheckedChange}
    >
      <CheckboxPrimitive.Indicator className="flex items-center justify-center text-black">
        <Tick01Icon className="w-4 h-4" />
      </CheckboxPrimitive.Indicator>
    </CheckboxPrimitive.Root>
    {label && <span className="text-sm text-gray-700">{label}</span>}
  </div>
);
