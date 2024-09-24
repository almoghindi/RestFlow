"use client";
import React from "react";
import DashboardSection from "./dashboard-section";
import { sectionsConfig } from "./sections";
import { LoadingSpinner } from "@/components/ui/loading-spinner";
import { Button } from "@/components/ui/button";
import useAuthRedirect from "@/hooks/use-auth-redirect";
import { Logout03Icon } from "hugeicons-react";
import { removeFromLocalStorage } from "@/lib/local-storage";
import { useRouter } from "next/navigation";
import { logout as logoutAction } from "@/store/slices/manager-slice";
import CustomModal from "./custom-modal";
import useManagerData from "./use-manager-data";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/store/store";

const ManagerPage: React.FC = () => {
  useAuthRedirect();
  const dispatch = useDispatch();
  const router = useRouter();
  const restaurant = useSelector((state: RootState) => state.restaurant);

  const {
    data,
    loading,
    error,
    isModalOpen,
    currentSection,
    currentItem,
    editMode,
    toggleModal,
    setCurrentSection,
    setCurrentItem,
    setEditMode,
    handleSave,
    handleDeleteItem,
  } = useManagerData(restaurant.id?.toString() || "");

  const handleTabClick = (section: string) => {
    setCurrentSection(section);
    setCurrentItem(null);
    setEditMode(false);
  };

  const logout = () => {
    removeFromLocalStorage("manager");
    dispatch(logoutAction());
    router.push("/restaurant");
  };

  return (
    <div
      className="flex flex-col items-center w-screen bg-cover bg-center relative"
      style={{
        backgroundImage: "url('/management.jpg')",
        height: "calc(100vh - 3rem)",
      }}
    >
      <div className="absolute inset-0 bg-black opacity-80" />
      <div className="relative z-10 flex flex-col items-center">
        <h1 className="text-4xl font-bold mb-6 mt-28 text-center">
          Restaurant Management
        </h1>
        <div className="mb-6 flex flex-wrap justify-center">
          {sectionsConfig.map((section) => (
            <Button
              key={section.name}
              onClick={() => handleTabClick(section.name)}
              variant={currentSection === section.name ? "ghost" : "outline"}
              className="mr-4 mb-2 text-lg py-2 px-6"
            >
              {section.name}
            </Button>
          ))}
        </div>
        <div className="mb-6 p-8">
          {loading ? (
            <LoadingSpinner />
          ) : error ? (
            <p className="text-red-500">Error fetching data.</p>
          ) : (
            <DashboardSection
              title={currentSection}
              items={data[currentSection] || []}
              fields={
                sectionsConfig.find((config) => config.name === currentSection)
                  ?.fields || []
              }
              onAdd={() => {
                setEditMode(false);
                setCurrentItem({});
                toggleModal();
              }}
              onEdit={
                currentSection === "Tables" || currentSection === "Waiters"
                  ? undefined
                  : (item) => {
                      setCurrentItem(item);
                      setEditMode(true);
                      toggleModal();
                    }
              }
              onDelete={(item) => handleDeleteItem(item)}
              categories={data.Categories}
            />
          )}
          <div className="mb-6 flex justify-end">
            <Button
              onClick={logout}
              className="w-max py-5 bg-red-500 text-white font-semibold rounded-lg hover:bg-red-600 mt-24"
            >
              <Logout03Icon className="text-3xl mr-2" />
              Logout
            </Button>
          </div>
        </div>
        <CustomModal
          title={editMode ? `Edit ${currentSection}` : `Add ${currentSection}`}
          isOpen={isModalOpen}
          onClose={toggleModal}
          onSave={handleSave}
          saveButtonText={editMode ? "Update" : "Save"}
          fields={
            sectionsConfig.find((config) => config.name === currentSection)
              ?.fields || []
          }
          currentItem={currentItem}
          setCurrentItem={setCurrentItem}
          categories={data.Categories}
          ingredients={data.Ingredients}
        />
      </div>
    </div>
  );
};

export default ManagerPage;
