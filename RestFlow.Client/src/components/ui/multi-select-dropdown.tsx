import * as React from "react";
import * as DropdownMenu from "@radix-ui/react-dropdown-menu";
import { ArrowDown01Icon, ArrowUp01Icon, Tick01Icon } from "hugeicons-react";
import { cn } from "@/lib/class-names";
import { cva, type VariantProps } from "class-variance-authority";

const dropdownVariants = cva(
  "relative flex items-center border rounded-md px-3 py-2 cursor-pointer text-sm ring-offset-background placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2",
  {
    variants: {
      variant: {
        default: "bg-background border",
        outline: "border-2 border-muted",
        filled: "bg-secondary text-white",
      },
      size: {
        default: "h-10",
        sm: "h-9 px-2 py-1",
        lg: "h-11 px-3",
      },
    },
    defaultVariants: {
      variant: "default",
      size: "default",
    },
  }
);

interface Option {
  value: string | number;
  label: string;
}

interface MultiSelectDropdownProps
  extends VariantProps<typeof dropdownVariants> {
  options: Option[];
  selectedValues: string[];
  onChange: (selectedValues: string[]) => void;
  placeholder?: string;
  className?: string;
  size?: "default" | "sm" | "lg";
}

const MultiSelectDropdown: React.FC<MultiSelectDropdownProps> = ({
  options,
  selectedValues = [],
  onChange,
  placeholder = "",
  variant = "default",
  size = "default",
  className = "",
}) => {
  const [open, setOpen] = React.useState(false);
  const triggerRef = React.useRef<HTMLButtonElement>(null);
  const [dropdownWidth, setDropdownWidth] = React.useState<number | undefined>(
    undefined
  );
  const handleSelect = (value: string) => {
    const newSelection = selectedValues.includes(value)
      ? selectedValues.filter((item) => item !== value)
      : [...selectedValues, value];

    onChange(newSelection);
  };

  const selectedLabels = selectedValues
    .map(
      (selectedValue) =>
        options.find((option) => option.value.toString() === selectedValue)
          ?.label
    )
    .filter(Boolean);

  React.useEffect(() => {
    if (triggerRef.current) {
      setDropdownWidth(triggerRef.current.offsetWidth);
    }
  }, [open]);

  return (
    <DropdownMenu.Root open={open} onOpenChange={setOpen}>
      <DropdownMenu.Trigger asChild>
        <button
          ref={triggerRef}
          className={cn(
            "flex items-center justify-between w-full border rounded-md px-3 py-2 bg-background text-white",
            className
          )}
        >
          <span className="flex-grow text-left text-sm">
            {selectedLabels.length > 0
              ? selectedLabels.join(", ")
              : placeholder}
          </span>
          {open ? (
            <ArrowUp01Icon className="h-5 w-5 text-gray-500" />
          ) : (
            <ArrowDown01Icon className="h-5 w-5 text-gray-500" />
          )}
        </button>
      </DropdownMenu.Trigger>

      <DropdownMenu.Portal>
        <DropdownMenu.Content
          align="start"
          sideOffset={4}
          style={{ width: dropdownWidth }}
          className="mt-1 bg-background text-white rounded-lg shadow-lg max-h-60 overflow-y-auto z-50"
        >
          {options.length === 0 ? (
            <div className="px-3 py-2 text-center">No options available</div>
          ) : (
            options.map((option) => (
              <DropdownMenu.Item
                key={option.value.toString()}
                onClick={() => handleSelect(option.value.toString())}
                className={cn(
                  "flex items-center px-3 py-2 hover:bg-gray-800 transition-all duration-200 cursor-pointer text-sm",
                  selectedValues.includes(option.value.toString())
                    ? "bg-gray-800"
                    : "bg-transparent"
                )}
              >
                <span className="flex-grow">{option.label}</span>
                {selectedValues.includes(option.value.toString()) && (
                  <Tick01Icon className="h-4 w-4 text-indigo-500" />
                )}
              </DropdownMenu.Item>
            ))
          )}
        </DropdownMenu.Content>
      </DropdownMenu.Portal>
    </DropdownMenu.Root>
  );
};
export { MultiSelectDropdown };
