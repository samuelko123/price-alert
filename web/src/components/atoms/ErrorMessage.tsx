import { Problem } from "@/errors/Problem";
import { ValidationProblem } from "@/errors/ValidationProblem";

export const ErrorMessage = ({
  error,
}: {
  error: Error,
}) => {
  if (error instanceof ValidationProblem) {
    return (
      <div>
        <p>Error:</p>
        <p>{error.title}</p>
        <ul>
          {error.errors.map((err, index) => <li key={index}>{err.message}</li>)}
        </ul>
      </div>
    );
  }

  if (error instanceof Problem) {
    return (
      <div>
        <p>Error:</p>
        <p>{error.title}</p>
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
