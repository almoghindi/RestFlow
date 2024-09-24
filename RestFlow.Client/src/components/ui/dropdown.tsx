import * as Select from "@radix-ui/react-select";
import { ArrowDown01Icon, ArrowUp01Icon, Tick01Icon } from "hugeicons-react";
import { cn } from "@/lib/class-names";
import React, { useState } from "react";

interface DropdownProps {
  label?: string;
  items: { value: string; name: string }[];
  value: string;
  onChange: (value: string) => void;
}

export function Dropdown({ label, items, value, onChange }: DropdownProps) {
  const [isOpen, setIsOpen] = useState(false);

  const selectedItem = items.find((item) => item.value === value);

  return (
    <div className="relative w-full max-w-md">
      {label && (
        <label className="block text-sm font-medium text-black">{label}</label>
      )}
      <Select.Root
        value={value}
        onValueChange={onChange}
        onOpenChange={setIsOpen}
      >
        <Select.Trigger
          className="w-full h-10 flex justify-between items-center rounded-md border border-gray-300 bg-background py-2 px-3 text-sm leading-4 shadow-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
          aria-label={label}
        >
          <Select.Value className="text-black">
            {selectedItem ? selectedItem.name : "Select an option"}
          </Select.Value>
          <Select.Icon>
            {isOpen ? (
              <ArrowUp01Icon className="h-5 w-5 text-gray-500" />
            ) : (
              <ArrowDown01Icon className="h-5 w-5 text-gray-500" />
            )}
          </Select.Icon>
        </Select.Trigger>
        <Select.Portal>
          <Select.Content
            className="z-50 w-full overflow-hidden bg-background rounded-md shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
            position="popper"
            sideOffset={5}
            align="start"
            avoidCollisions
          >
            <Select.Viewport className="p-2">
              {items.map((item) => (
                <Select.Item
                  key={item.value}
                  value={item.value}
                  className={cn(
                    "relative flex items-center py-3 pl-3 pr-36 text-sm text-white rounded-md cursor-pointer select-none",
                    "radix-disabled:opacity-50 radix-highlighted:bg-indigo-600 radix-highlighted:text-white"
                  )}
                >
                  <Select.ItemText>{item.name}</Select.ItemText>
                  <Select.ItemIndicator className="absolute inset-y-0 right-0 flex items-center pr-4">
                    <Tick01Icon className="h-5 w-5 text-indigo-600" />
                  </Select.ItemIndicator>
                </Select.Item>
              ))}
            </Select.Viewport>
          </Select.Content>
        </Select.Portal>
      </Select.Root>
    </div>
  );
}
