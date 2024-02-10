# WordFinderTest
 This project was made to solve the Word finder exercise. It is an API and there are 2 POST endpoints that you can use:

- POST /WordFinder/findAll
- POST /WordFinder/findTop10 

The request object for both endpoints will be like this:

```
{
  "matrix": [
    "abcdc", "fgwio", "chill", "pqnsd", "uvdxy"
  ],
  "wordstream": [
    "cold", "snow", "chill", "wind"
  ]
}
```
Where the parameters are:
- matrix: Set of strings which represents a character matrix. The matrix size does not exceed 64x64, all strings contain the same number of characters.
- wordstream: Set of strings with words to be found in the matrix.
  
  
In the first one, findAll, you will find a response object with all the words you were finding in the matrix, and a counter with the number of coincidences for each word, even if there is 0 coincidences. This endpoint was added as a tool to validate the algorithm, to see how many times each word is found. The response object will be like this:
```
[
  {
    "word": "cold",
    "count": 1
  },
  {
    "word": "chill",
    "count": 1
  },
  {
    "word": "wind",
    "count": 1
  },
  {
    "word": "snow",
    "count": 0
  }
]
```

The other endpoint, findTop10, is the one that solves the exercise as requested, it means, it returns an array with the 10 most repeated words.

The solution has 2 projects, the first one is called WordFinder and is where the API lives, so this is the one you will need to run when you want to execute the API on your own computer. The second one is called UnitTests and it will run some unit tests cases that were designed for this exercise.
