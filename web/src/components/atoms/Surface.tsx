import { ReactNode } from "react";

export const Surface = ({ children }: { children: ReactNode }) => {
  return (
    <div className="bg-surface rounded-lg px-2 py-2">
      {children}
    </div>
  );
};
