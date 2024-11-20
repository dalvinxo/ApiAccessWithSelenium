export interface CommentGame {
  content: string;
  id: number;
  parentCommentId?: number | null;
  createdDate?: string;
}
