import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoaderService } from '../../Service/loader.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loader',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.css'
})
export class LoaderComponent {
  isLoading = false;
  private subscription!: Subscription;

  constructor(private loaderService:LoaderService) {}

  ngOnInit(): void {
    this.subscription = this.loaderService.isLoading.subscribe(
      (Loading) => {
        this.isLoading = Loading;
      }
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
