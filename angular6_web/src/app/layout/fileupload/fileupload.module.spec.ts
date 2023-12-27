import { FileuploadModule } from './fileupload.module';

describe('FileuploadModule', () => {
  let fileuploadModule: FileuploadModule;

  beforeEach(() => {
    fileuploadModule = new FileuploadModule();
  });

  it('should create an instance', () => {
    expect(fileuploadModule).toBeTruthy();
  });
});
