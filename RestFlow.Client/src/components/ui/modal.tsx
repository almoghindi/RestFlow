import * as React from "react";
import { Slot } from "@radix-ui/react-slot";
import { cva, type VariantProps } from "class-variance-authority";
import { cn } from "@/lib/class-names";
import {
  Dialog,
  DialogOverlay,
  DialogContent,
  DialogPortal,
  DialogTitle,
} from "@radix-ui/react-dialog";
import { X } from "lucide-react";
import { Button } from "./button";

const modalVariants = cva(
  "fixed left-[50%] top-[50%] z-50 grid w-full max-w-lg translate-x-[-50%] translate-y-[-50%] gap-4 border border-gray-200 bg-white p-6 shadow-lg duration-200 data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0 data-[state=closed]:zoom-out-95 data-[state=open]:zoom-in-95 data-[state=closed]:slide-out-to-left-1/2 data-[state=closed]:slide-out-to-top-[48%] data-[state=open]:slide-in-from-left-1/2 data-[state=open]:slide-in-from-top-[48%] sm:rounded-lg",
  {
    variants: {
      size: {
        default: "w-full max-w-md",
        sm: "w-full max-w-sm",
        lg: "w-full max-w-lg",
      },
      variant: {
        default: "bg-white",
        dark: "bg-gray-800 text-white border-gray-700",
      },
    },
    defaultVariants: {
      size: "default",
      variant: "default",
    },
  }
);

export interface ModalProps
  extends VariantProps<typeof modalVariants>,
    React.ComponentPropsWithoutRef<typeof Dialog> {
  title: string;
  isOpen: boolean;
  onClose: () => void;
  asChild?: boolean;
  onSave?: () => void;
  saveButtonText?: string;
}

const Modal = React.forwardRef<React.ElementRef<typeof Dialog>, ModalProps>(
  (
    {
      title,
      isOpen,
      onClose,
      size,
      variant,
      asChild = false,
      children,
      onSave,
      saveButtonText = "Save",
      ...props
    },
    ref
  ) => {
    const Comp = asChild ? Slot : Dialog;
    return (
      <Comp ref={ref} open={isOpen} onOpenChange={onClose} {...props}>
        <DialogPortal>
          <DialogOverlay className="fixed inset-0 z-50 bg-black/50 backdrop-blur-sm data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0" />
          <DialogContent className={cn(modalVariants({ size, variant }))}>
            <div className="flex flex-col space-y-1.5 text-center sm:text-left">
              <DialogTitle className="text-lg font-semibold leading-none tracking-tight text-black">
                {title}
              </DialogTitle>
            </div>
            <button
              type="button"
              onClick={onClose}
              className="absolute right-4 top-4 rounded-sm opacity-70 ring-offset-background transition-opacity hover:opacity-100 focus:outline-none focus:ring-2 focus:ring-ring focus:ring-offset-2 disabled:pointer-events-none data-[state=open]:bg-accent data-[state=open]:text-muted-foreground"
            >
              <X className="h-4 w-4 text-black" />
              <span className="sr-only">Close</span>
            </button>
            <div className="max-h-[60vh] overflow-y-auto pr-2">{children}</div>
            <div className="flex justify-end space-x-2 pt-4">
              <Button type="button" onClick={onClose} variant="ghost">
                Cancel
              </Button>
              {onSave && (
                <Button type="button" onClick={onSave} variant="outline">
                  {saveButtonText}
                </Button>
              )}
            </div>
          </DialogContent>
        </DialogPortal>
      </Comp>
    );
  }
);

Modal.displayName = "Modal";

export { Modal };
