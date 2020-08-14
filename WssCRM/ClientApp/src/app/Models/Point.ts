import { Stage } from "./Stage";

export class Point {
  constructor(
    public name?: string,
    public value?: number,
    public id?: number,
    public maxMark?: number,
    public active?: boolean,
    public red?: boolean,
    public deleted?: boolean,
    public stage?: Stage,
    public num ?: number
  ) {
  this.active = false;

    this.deleted = false;
  }
}
