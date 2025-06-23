import { Component, inject, OnInit } from '@angular/core';
import { Header } from './layout/header/header';
import { ShopComponent } from './features/shop/shop';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, ShopComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  title = 'Skinet';
}
