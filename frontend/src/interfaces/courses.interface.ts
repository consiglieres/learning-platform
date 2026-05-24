export interface ICourseCategory {
  type: string;
  value: string;
}

export interface IModule {
  name: string;
  moduleOrder: number;
  courseId: string;
  lessonIds: string[];
  introductionPageId: string;
  id: string;
  createdAt: string;
  createdBy: null | IUserShort;
  updatedAt: null | string;
  updatedBy: null | IUserShort;
  deletedAt: null | string;
  deletedBy: null | IUserShort;
}

export interface IContentBlock {}

export interface IIntroductionPage {
  order: number;
  type: null | string;
  contentBlocks: IContentBlock[];
  version: null | number;
  id: string;
  createdAt: string;
  createdBy: null | IUserShort;
  updatedAt: null | string;
  updatedBy: null | IUserShort;
  deletedAt: null | string;
  deletedBy: null | IUserShort;
}

export interface IUserShort {
  id: string;
  email: string;
  lastLoginAt: null | string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface ICourseFull {
  title: string;
  description: string;
  categories: ICourseCategory[];
  modules: IModule[];
  introductionPage: IIntroductionPage;
  moderationComment: null | string;
  submittedForModerationAt: null | string;
  submittedBy: IUserShort;
  publishedAt: null | string;
  publishedBy: IUserShort;
  status: number;
  version: { order: number; tag: null | string };
  id: string;
  createdAt: string;
  createdBy: IUserShort;
  updatedAt: null | string;
  updatedBy: IUserShort;
  deletedAt: null | string;
  deletedBy: IUserShort;
}

export interface ICourse {
  id: string;
  title: string;
  description: string;
  status: number;
  categories: ICourseCategory[];
  image?: string;
  duration?: number;
  tasks?: number;
  language?: string;
}

export interface ICourseDraft {
  title: string;
  description: string;
  categories: ICourseCategory[];
  modules?: {   // опционально, согласно API
    name: string;
    moduleOrder: number;
    courseId: string;
    lessonIds: string[];
    introductionPageId: string;
  }[];
}

export interface ICourseUpdate {
  title?: string | null;
  description?: string | null;
  categories?: ICourseCategory[] | null;
}

export interface IModerationComment {
  comment: string;
}

export interface ITopic {
  id: string;
  title: string;
  time: string;
  task?: ITask[];
  description: string;
}

export interface ITask {
  id: string;
  title: string;
  points: number;
  type?: 'quiz' | 'code' | 'theory';
  completed?: boolean;
  codeStarter?: string;
  codeSolution?: string;
  codeLanguage?: string;
  taskDescription?: string;
}

