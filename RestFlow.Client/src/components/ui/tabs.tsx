import * as React from "react";
import { Slot } from "@radix-ui/react-slot";
import { cva, type VariantProps } from "class-variance-authority";
import { cn } from "@/lib/class-names";
import { Tabs as RadixTabs, TabsContent } from "@radix-ui/react-tabs";

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

export interface TabContentProps
  extends React.ComponentPropsWithoutRef<typeof TabsContent> {}

const TabContent = React.forwardRef<
  React.ElementRef<typeof TabsContent>,
  TabContentProps
>((props, ref) => {
  return <TabsContent ref={ref} {...props} />;
});

TabContent.displayName = "TabContent";

export { Tabs, TabContent };
