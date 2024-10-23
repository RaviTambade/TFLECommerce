import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Product {
  productId: number;
  productTitle: string;
  description: string;
  category: string;
  unitprice: number;
  stockAvailable:number
}



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  public products: Product[] = [];


  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
    this.getProducts();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getProducts() {

    this.http.get<Product[]>('http://localhost:5268/api/products').subscribe(
      (result) => {
        this.products = result;
        console.log(result);
      },

      (error) => {
        console.error(error);
      }
    );
  }


  title = 'shoppingstoreapp.client';
}
