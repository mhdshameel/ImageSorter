# ImageSorter
## Contents
1. [Summary](#summary) 
2. [Usage](#usage)
3. [Plan](#plan)
4. [Contribution](#contribution)

## Summary:
A simple utility application for Windows that lets you view image files (jpeg, png, tiff and other formats) and sort them manually into subfolders directly from the application. You can also crop, rotate and convert to greyscale and save them.

I created the application basically to learn more about the dotnet WPF technology and MVVM pattern. 
Also planning to bring in more features if this works out well.

## Usage:
[Here](https://drive.google.com/file/d/1in1zf1M5ZuHHINVBQYajiX0Yy-wP39MC/view?usp=sharing) is a demo video which covers all the existing functionalities.

![image](https://user-images.githubusercontent.com/16662695/215321123-aaeb0699-5db3-4680-813e-965330870104.png)
Open a folder using the top right open button and browsing to a directory that contains images.


Wait for the images in the selected directory to load. Then you will see a similar display:
![image](https://user-images.githubusercontent.com/16662695/215321559-88a3f059-ecc3-4b1a-984a-95fae68b8f95.png)


Play with the detailed view mode by selecting it in the combo box. You can see properties of the image you selected.
![image](https://user-images.githubusercontent.com/16662695/215321621-def527f3-e1a2-4778-aa78-b08af3e8d989.png)


Double click on any image to open the editing panel, where you can Crop, Rotate and convert to greyscale. Just close for the changes to apply to the image view, note that the original image is not affected. I know it is a very bad user experience, I am open to suggestions and improvements.
![image](https://user-images.githubusercontent.com/16662695/215321950-22ef25fc-b941-4fcf-bfe8-d9ea261a987e.png)


The subdirectories in the directory you select will be populated in the bottom drop down, select any subdirectory or create a new one and copy the selected image into it.

![image](https://user-images.githubusercontent.com/16662695/215321708-73974da3-6cf9-46a4-a600-5f16b21318ff.png)

## Plan
I wish to collaboratively improve this application. The usability of the application should be improved a lot. The editing panel has scope for more functionalities. I am thinking of bringing in OCR capabilities as well.

## Contribution
This is a very early stage of the application and there is a large scope for contribution. You are welcome to contribute, I would appreciate it very much. 
Just check the issues and submit a PR. Or raise your ideas or feature requests as a Issue and I will definitely check it out.
