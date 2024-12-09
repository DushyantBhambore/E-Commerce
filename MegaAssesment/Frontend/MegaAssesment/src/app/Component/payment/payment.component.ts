import { Component, Inject, inject } from '@angular/core';
import { CartService } from '../../Service/cart.service';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule,CurrencyPipe,FormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent {

  service = inject(CartService)

  cardNumber = '';
  expiryDate = '';
  cvv = '';

  constructor(
    public dialogRef: MatDialogRef<PaymentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  pay() {
    if (this.cardNumber.length === 16  && this.cvv.length === 3 ) {
      this.dialogRef.close({
        cardNumber: this.cardNumber,
        expiryDate: this.expiryDate,
        cvv: this.cvv
      });
    } 
    else {
      alert('Invalid card details');
    }
  }


  
}
