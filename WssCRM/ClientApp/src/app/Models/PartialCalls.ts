import { Call } from "./Call";

export class PartialCalls {
  constructor(
    public calls: Call[], 
    public pageSize: number
  )
  {
    this.calls = [];
    this.pageSize = 0;
  }
}
