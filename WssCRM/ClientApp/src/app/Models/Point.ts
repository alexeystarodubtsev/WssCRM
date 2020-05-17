export class Point {
  constructor(
    public name?: string,
    public value?: number,
    public id?: number,
    public maxMark?: number,
    public active?: boolean

  ) { this.active = false; }
}
