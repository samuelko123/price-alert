export class Problem extends Error {
  public title: string;

  constructor({
    title,
  }: {
    title: string,
  }) {
    super(title);
    this.title = title;
  }
}
