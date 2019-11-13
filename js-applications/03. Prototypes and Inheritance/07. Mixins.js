function result() {

    function computerQualityMixin(cs) {
        cs.prototype.getQuality = function (params) {

            return (this.processorSpeed + this.ram + this.hardDiskSpace) / 3;
        }

        cs.prototype.isFast = function () {
            return (this.processorSpeed > (this.ram / 3));
        }

        cs.prototype.isRoomy = function () {
            return (this.hardDiskSpace > Math.floor(this.ram * this.processorSpeed));
        }
    }

    function styleMixin(cs) {
        cs.prototype.isFullSet = function () {
            return (this.manufacturer === this.keyboard.manufacturer &&
                this.manufacturer === this.monitor.manufacturer);
        }

        cs.prototype.isClassy = function () {
            var batteryValid = this.battery && this.battery.expectedLife >= 3;
            var validColor = this.color === 'Silver' || this.color === 'Black';
            var validWeight = this.weight < 3;

            return validColor && validWeight && batteryValid;
        }
    }

    return { computerQualityMixin, styleMixin }
} 
