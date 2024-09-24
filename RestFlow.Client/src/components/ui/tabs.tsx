import * as React from "react";
import { Slot } from "@radix-ui/react-slot";
import { cva, type VariantProps } from "class-variance-authority";
import { cn } from "@/lib/class-names";
import {
  Tabs as RadixTabs,
  TabsContent,
  TabsTrigger,
  TabsList,
} from "@radix-ui/react-tabs";

const tabsVariants = cva("flex w-full", {
  variants: {
    size: {
      default: "h-10 px-3",
      sm: "h-8 px-2",
      lg: "h-12 px-4",
    },
  },
  defaultVariants: {
    size: "default",
  },
});

export interface TabsProps
  extends VariantProps<typeof tabsVariants>,
    React.ComponentPropsWithoutRef<typeof RadixTabs> {
  asChild?: boolean;
}

const Tabs = React.forwardRef<React.ElementRef<typeof RadixTabs>, TabsProps>(
  ({ className, size, asChild = false, ...props }, ref) => {
    const Comp = asChild ? Slot : RadixTabs;
    return (
      <Comp
        ref={ref}
        className={cn(tabsVariants({ size, className }))}
        {...props}
      />
    );
  }
);

Tabs.displayName = "Tabs";

export const CustomTabsList = React.forwardRef<
  React.ElementRef<typeof TabsList>,
  React.ComponentPropsWithoutRef<typeof TabsList>
>(({ className, ...props }, ref) => {
  return (
    <TabsList ref={ref} className={cn("flex flex-col", className)} {...props} />
  );
});
CustomTabsList.displayName = "CustomTabsList";

export const CustomTabsTrigger = React.forwardRef<
  React.ElementRef<typeof TabsTrigger>,
  React.ComponentPropsWithoutRef<typeof TabsTrigger>
>(({ className, ...props }, ref) => {
  return (
    <TabsTrigger
      ref={ref}
      className={cn(
        "py-4 hover:bg-gray-700 rounded text-2xl font-bold",
        className
      )}
      {...props}
    />
  );
});
CustomTabsTrigger.displayName = "CustomTabsTrigger";

export const CustomTabContent = React.forwardRef<
  React.ElementRef<typeof TabsContent>,
  React.ComponentPropsWithoutRef<typeof TabsContent>
>(({ className, ...props }, ref) => {
  return <TabsContent ref={ref} className={cn("p-4", className)} {...props} />;
});
CustomTabContent.displayName = "CustomTabContent";

export {
  Tabs,
  CustomTabContent as TabContent,
  CustomTabsList as TabsList,
  CustomTabsTrigger as TabsTrigger,
};
