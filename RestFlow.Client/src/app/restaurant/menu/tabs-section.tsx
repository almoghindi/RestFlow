import React from "react";
import { Tabs, TabsList, TabsTrigger } from "@/components/ui/tabs";

interface TabsSectionProps {
  categories: any[];
  activeTab: string | null;
  onTabClick: (categoryId: string) => void;
}

const TabsSection: React.FC<TabsSectionProps> = ({
  categories,
  activeTab,
  onTabClick,
}) => {
  return (
    <Tabs className="w-1/4 p-4 rounded-lg flex flex-col justify-between space-y-4">
      <TabsList className="space-y-4">
        {categories.map((category, index) => (
          <TabsTrigger
            key={category.id || index}
            value={category.id}
            onClick={() => onTabClick(category.categoryId)}
            className={`relative py-4 bg-gray-900 hover:bg-gray-700 rounded text-lg font-bold 
              transition-all duration-300 overflow-hidden
              ${activeTab === category.categoryId ? "bg-gray-700" : ""}`}
          >
            {category.name}
            <div className="absolute top-1 right-1 w-4 h-4 opacity-20">
              <div className="absolute top-0 right-0 w-full h-0.5 bg-white" />
              <div className="absolute top-0 right-0 h-full w-0.5 bg-white" />
            </div>
            <div className="absolute bottom-1 left-1 w-4 h-4 opacity-20">
              <div className="absolute bottom-0 left-0 w-full h-0.5 bg-white" />
              <div className="absolute bottom-0 left-0 h-full w-0.5 bg-white" />
            </div>
          </TabsTrigger>
        ))}
      </TabsList>
    </Tabs>
  );
};

export default TabsSection;
