import { Component, OnInit, Input, Output } from '@angular/core';
import { Stage } from '../Models/Stage';
import { Point } from '../Models/Point'
@Component({
  selector: 'app-stage',
  templateUrl: './stage.component.html',
  //providers: [DataService]
})
export class StageComponent implements OnInit {

  @Input() stage: Stage;
  MaxPoint: number = 0;
  curPoint: Point = new Point();
  curID: number = 0; //Поменять после бэка

  constructor() {  }
  ngOnInit() {

  }
  newPoint() {
    this.curPoint = new Point();
    this.curID += 1;
    this.curPoint.id = this.curID;

    this.stage.points.push(this.curPoint);
  }
  updateTotalData() {
    this.MaxPoint = 0;
    this.stage.points.forEach(p => { if (p.maxMark != undefined) this.MaxPoint += p.maxMark; });
  }
  EditPoint(p: Point) {
    this.curPoint = p;
  }
  savePoint() {
    this.curPoint = new Point();
    this.updateTotalData();
  }

}

