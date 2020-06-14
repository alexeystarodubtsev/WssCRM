export class missedCall {
  constructor(
    public id?: number,
    public clientName?: string,
    public clientLink?: string,
    public clientState?: string,
    public date?: string,
    public reason?: string,
    public correction?: string,
    public noticeCRM?: string,
    public dateNext?: string,
    public manager?: string,
    public processed? : boolean
  )
  {
    this.processed = false;
  }
}
