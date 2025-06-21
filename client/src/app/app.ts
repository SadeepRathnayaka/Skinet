import { Component, inject, OnInit } from '@angular/core';
import { Header } from './layout/header/header';
import { ShopComponent } from './features/shop/shop';

@Component({
  selector: 'app-root',
  imports: [Header, ShopComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  title = 'Skinet';
}
