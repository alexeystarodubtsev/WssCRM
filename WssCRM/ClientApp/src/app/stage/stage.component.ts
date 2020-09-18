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

  constructor() { }
  ngOnInit() {
    this.updateTotalData();
  }
  newPoint() {
    this.curPoint = new Point();
    this.curPoint.active = true;
    this.curPoint.num = 1;
    this.stage.points.forEach(p => { if (p.num >= this.curPoint.num) this.curPoint.num = p.num + 1; });
    this.stage.points.push(this.curPoint);
  }

  deletePoint(p: Point) {
    p.deleted = true;
    p.num *= -1;
  }

  updateTotalData() {
    this.MaxPoint = 0;
    this.stage.points.forEach(p => { if (p.maxMark != undefined) this.MaxPoint += p.maxMark; });
  }
  EditPoint(p: Point) {
    this.curPoint = p;
    this.curPoint.active = true;
  }
  savePoint() {
    this.curPoint.active = false;
    this.curPoint = null;
    this.updateTotalData();
  }

}

