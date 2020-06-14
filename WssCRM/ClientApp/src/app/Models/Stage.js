"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Stage = /** @class */ (function () {
    function Stage(name, points, id, deleted, agreementStage, preAgreementStage, incomeStage, num) {
        this.name = name;
        this.points = points;
        this.id = id;
        this.deleted = deleted;
        this.agreementStage = agreementStage;
        this.preAgreementStage = preAgreementStage;
        this.incomeStage = incomeStage;
        this.num = num;
        this.points = [];
        this.deleted = false;
        this.agreementStage = false;
        this.incomeStage = false;
        this.preAgreementStage = false;
    }
    return Stage;
}());
exports.Stage = Stage;
//# sourceMappingURL=Stage.js.map