import { Component } from '@angular/core';
import { Post } from 'src/app/models/post.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css'],
})
export class PostsComponent {
  posts: Post[] = [];
  newCommentContent: string = '';
  errorMessage: string | null = null;

  constructor(private postService: PostService) {}

  validateComment(): void {
    const specialCharacters = /[!@#$%^&*(),.?":{}|<>]/;
    const offensiveWords = /\b(maldito|obeso|cerdo)\b/i;

    if (specialCharacters.test(this.newCommentContent)) {
      this.errorMessage =
        'No se permiten caracteres especiales en el comentario.';
    } else if (offensiveWords.test(this.newCommentContent)) {
      this.errorMessage = 'El comentario contiene palabras inapropiadas.';
    } else if (!this.newCommentContent.trim()) {
      this.errorMessage = 'El comentario no puede estar vacío.';
      return;
    } else {
      this.errorMessage = null;
    }
  }

  ngOnInit(): void {
    this.fetchPosts();
  }

  fetchPosts(): void {
    this.postService.getPosts().subscribe((data) => {
      this.posts = data;
    });
  }

  addComment(postId: number): void {
    this.validateComment();

    if (this.errorMessage) {
      return;
    }

    if (postId == 0) {
      this.errorMessage = 'No se ha seleccionado un post';
      return;
    }

    this.postService
      .addComment(postId, this.newCommentContent)
      .subscribe(() => {
        this.fetchPosts();
        this.newCommentContent = '';
        this.errorMessage = null;
      });
  }
}
