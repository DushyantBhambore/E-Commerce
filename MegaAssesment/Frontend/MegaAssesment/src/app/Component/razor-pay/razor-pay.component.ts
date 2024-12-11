import { Component, OnInit } from '@angular/core';
import { RazorService } from '../../Service/razor.service';
declare var Razorpay: any; // Add this line if Razorpay is not globally available


@Component({
  selector: 'app-razor-pay',
  standalone: true,
  imports: [],
  templateUrl: './razor-pay.component.html',
  styleUrl: './razor-pay.component.css'
})
export class RazorPayComponent implements OnInit {

  constructor(private paymentService: RazorService) { }
  ngOnInit(): void {}
  onPayNow(amount: number) {
    debugger;
    this.paymentService.createOrder(amount).subscribe((order:any) => {
      const options: any = {
        key:'rzp_test_KMY5pBLSVhJH3u', // Replace with your Razorpay Key ID
        amount: amount * 100, // Amount in paise
        currency: 'INR',
        name: 'SDN Company',
        description: 'Payment for Order',
        order_id: order.orderId,
        handler: (response: any) => {
          // this.verifyPayment(response);
        },
        prefill: {
          name: 'Customer Name',
          email: 'customer@example.com',
        },
        theme: {
          color: '#F37254'
        }
      };
      
      const rzp1 :any = new Razorpay(options);
      rzp1.open();
    });
  }

  verifyPayment(response: any) {
    debugger
    const paymentData = {
      paymentId: response.razorpay_payment_id,
      orderId: response.razorpay_order_id,
      signature: response.razorpay_signature
    };
    this.paymentService.verifyPayment(paymentData)
    .subscribe(result => {
      alert('Payment successful!');
    }, error => {
      alert('Payment verification failed.');
    });

  }
}



