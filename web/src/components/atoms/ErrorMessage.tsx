import { Problem } from "@/errors/Problem";

export const ErrorMessage = ({
  error,
}: {
  error: Error,
}) => {
  if (error instanceof Problem) {
    return (
      <div>
        <p>Error:</p>
        <p>{error.message}</p>
      </div>
    );
  }

  return (
    <div>
      <p>Error:</p>
      <p>Something went wrong.</p>
    </div>
  );
};
