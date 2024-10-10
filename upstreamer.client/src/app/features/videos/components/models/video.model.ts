export type Video = {
    id: number;
    title: string;
    description: string|null;
    category: string;
    // thumbnail: string; // TODO: once thumbnail feature is available
  };
  
export type VideoResponse = {
    videos: Video[];
    total: number;
};
