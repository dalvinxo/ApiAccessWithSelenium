import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post.model';
import { GameForm } from '../models/gameForm.model';
import { CommentGame } from '../models/commet.model';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  private apiUrl: string = environment.apiUrl + '/posts';
  private commentsApiUrl: string = environment.apiUrl + '/comments';

  constructor(private http: HttpClient) {}

  getPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.apiUrl);
  }

  createPost(model: GameForm): Observable<Post> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Post>(`${this.apiUrl}`, model, { headers });
  }

  addComment(
    postId: number,
    content: string,
    parentCommentId: number | null = null
  ): Observable<CommentGame> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const commentPayload = { postId, content, parentCommentId };

    console.log(commentPayload);

    return this.http.post<CommentGame>(this.commentsApiUrl, commentPayload, {
      headers,
    });
  }
}
