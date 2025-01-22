import { Component, OnInit } from '@angular/core';

import { HackerNewsService } from './hacker-news.service';

@Component({
  selector: 'app-hacker-news',
  standalone: false,
  templateUrl: './hacker-news.component.html',
  styleUrl: './hacker-news.component.css'
})
export class HackerNewsComponent implements OnInit {
  allItems: any[] = [];
  itemIds: number[] = [];

  currentPage: number = 1;
  pageSize: number = 25;
  totalItems: number = 0;

  searchTerm: string = '';
  filteredItems: any[] = [];

  constructor(private hackerNewsService: HackerNewsService) { }

  ngOnInit(): void {
    this.fetchTopItems();
  }

  private fetchTopItems(): void {
    this.hackerNewsService.getTopItems().subscribe((ids) => {
      this.itemIds = ids;
      this.totalItems = ids.length;
      this.itemIds.forEach((id) => {
        this.hackerNewsService.getItem(id).subscribe((item) => {
          this.allItems.push(item);
          this.filteredItems = [...this.allItems];
        });
      });
    });
  }

  changePage(page: number): void {
    if (page >= 1 && page <= Math.ceil(this.totalItems / this.pageSize)) {
      this.currentPage = page;
    }
  }

  searchItems(): void {
    this.currentPage = 1;
    const search = this.searchTerm.toLowerCase();
    this.filteredItems = this.allItems.filter(
      (item) => item.title.toLowerCase().includes(search) || item.by.toLowerCase().includes(search)
    );
    this.totalItems = this.filteredItems.length;
  }

  get currentPageItems(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;

    return this.filteredItems.slice(startIndex, endIndex);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }
}
