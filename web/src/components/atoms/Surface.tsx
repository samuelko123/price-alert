import { ReactNode } from "react";

export const Surface = ({ children }: { children: ReactNode }) => {
  return (
    <div className="bg-surface rounded-lg px-4 py-4">
      {children}
    </div>
  );
};
