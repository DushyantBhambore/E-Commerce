import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-address',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './address.component.html',
  styleUrl: './address.component.css'
})
export class AddressComponent {
  address = '';

  constructor(public dialogRef: MatDialogRef<AddressComponent>) {}

  saveAddress() {
    this.dialogRef.close(this.address);
  }


}
