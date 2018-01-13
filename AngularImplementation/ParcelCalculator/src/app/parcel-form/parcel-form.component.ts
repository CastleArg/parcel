import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, ValidatorFn, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-parcel-form',
  templateUrl: './parcel-form.component.html',
  styleUrls: ['./parcel-form.component.css']
})
export class ParcelFormComponent implements OnInit {
  maxWeight = 25;
  volumeSmallMm = 200 * 300 * 150;
  volumeMedMm = 300 * 400 * 200;
  volumeLargeMm = 400 * 600 * 250;
  form: FormGroup;
  height = new FormControl('', [Validators.required, Validators.min(0), Validators.max(600)]);
  width = new FormControl('', [Validators.required, Validators.min(0), Validators.max(600)]);
  breadth = new FormControl('', [Validators.required, Validators.min(0), Validators.max(600)]);
  weight = new FormControl('', [Validators.required, Validators.min(0), Validators.max(25)]);
  cost: number;
  type: string;
  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      'height': this.height,
      'width': this.width,
      'breadth': this.breadth,
      'weight': this.weight,
    })
  }

  calculateType() {
    if (this.weight.value > this.maxWeight) {
      this.form.setErrors({ 'maxWeightExceeded': true });
      return;
    }

    let volume = this.height.value * this.width.value * this.breadth.value
    console.log(volume);
    if (volume > this.volumeLargeMm) {
      this.form.setErrors({ 'maxDimensionsExceeded': true });
      return;
    }

    if (!this.form.valid) {
      this.form.setErrors({ 'fillAllFields': true });
      return;
    }

    if (volume > this.volumeMedMm) {
      this.type = 'Large';
      this.cost = 8.50;
      return;
    }
    if (volume > this.volumeSmallMm) {
      this.type = 'Medium';
      this.cost = 7.50;
      return;
    }

    this.type = 'Small';
    this.cost = 5.00;
    return;
  }
}
