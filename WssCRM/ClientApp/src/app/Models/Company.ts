export class Company {

  constructor(
    public name?: string,
    public managers?: string[]
    
  ) {
    this.managers = [];
    }
}
