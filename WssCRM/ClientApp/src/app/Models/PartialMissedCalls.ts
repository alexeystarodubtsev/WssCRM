import { missedCall } from "./missedCall";

export class PartialMissedCalls {
  constructor(
    public calls: missedCall[], 
    public pageSize: number
  )
  {
    this.calls = [];
    this.pageSize = 0;
  }
}
