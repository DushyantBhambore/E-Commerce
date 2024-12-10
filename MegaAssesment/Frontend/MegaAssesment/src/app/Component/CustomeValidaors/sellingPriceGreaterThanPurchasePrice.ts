import { AbstractControl, ValidationErrors } from '@angular/forms';

export function sellingPriceGreaterThanPurchasePrice(control: AbstractControl): ValidationErrors | null {
  const sellingPrice = control.get('sellingPrice')?.value;
  const purchasePrice = control.get('purchasePrice')?.value;

  if (sellingPrice <= purchasePrice) {
    return { 'sellingPriceGreaterThanPurchasePrice': true };
  }

  return null;
}
