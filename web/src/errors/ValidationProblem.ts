import { Problem } from "./Problem";

export class ValidationProblem extends Problem {
  public errors: { message: string }[];

  constructor({
    title,
    errors,
  }: {
    title: string,
    errors: { message: string }[],
  }) {
    super({ title });
    this.errors = errors;
  };
}
