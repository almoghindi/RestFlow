import * as React from "react";
import { cva, type VariantProps } from "class-variance-authority";
import { cn } from "@/lib/class-names";

const spinnerVariants = cva(
  "inline-flex items-center justify-center animate-spin rounded-full",
  {
    variants: {
      size: {
        sm: "h-4 w-4 border-2",
        default: "h-6 w-6 border-2",
        lg: "h-8 w-8 border-4",
      },
      color: {
        primary:
          "border-t-primary border-b-primary border-r-primary border-l-transparent",
        secondary:
          "border-t-secondary border-b-secondary border-r-secondary border-l-transparent",
        default:
          "border-t-gray-500 border-b-gray-500 border-r-gray-500 border-l-transparent",
      },
    },
    defaultVariants: {
      size: "default",
      color: "default",
    },
  }
);

export interface SpinnerProps extends VariantProps<typeof spinnerVariants> {
  asChild?: boolean;
}

const LoadingSpinner = React.forwardRef<HTMLDivElement, SpinnerProps>(
  ({ size, color, asChild = false, ...props }, ref) => {
    const Comp = asChild ? "span" : "div";
    return (
      <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50">
        <Comp
          className={cn(spinnerVariants({ size, color }))}
          ref={ref}
          {...props}
        />
      </div>
    );
  }
);
LoadingSpinner.displayName = "Spinner";

export { LoadingSpinner };
