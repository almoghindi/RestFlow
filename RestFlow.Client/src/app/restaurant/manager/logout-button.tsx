import React from "react";
import { Button } from "@/components/ui/button";
import { Logout03Icon } from "hugeicons-react";

interface LogoutButtonProps {
  onLogout: () => void;
}

const LogoutButton: React.FC<LogoutButtonProps> = ({ onLogout }) => {
  return (
    <div className="mb-6 flex justify-end">
      <Button
        onClick={onLogout}
        className="w-max py-5 bg-red-500 text-white font-semibold rounded-lg hover:bg-red-600 mt-24"
      >
        <Logout03Icon className="text-3xl mr-2" />
        Logout
      </Button>
    </div>
  );
};

export default LogoutButton;
