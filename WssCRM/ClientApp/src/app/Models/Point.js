"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Point = /** @class */ (function () {
    function Point(name, value, id, maxMark, active, deleted) {
        this.name = name;
        this.value = value;
        this.id = id;
        this.maxMark = maxMark;
        this.active = active;
        this.deleted = deleted;
        this.active = false;
        this.deleted = false;
    }
    return Point;
}());
exports.Point = Point;
//# sourceMappingURL=Point.js.map