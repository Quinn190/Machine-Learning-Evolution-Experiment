# Machine-Learning-Evolution-Experiment


##Example Gif format
![](https://github.com/Quinn190/Machine-Learning-Evolution-Experiment/creature-vision-image.jpg?raw=true)


So this is a project I started developing after being intrigued by [General AI?].


The creatures have a 5 x 4 grid for vision.

At the start, 40 creatures are spawned with random weights assigned. They are assessed based on a primary fitness function of distance, and a secondary function of time. 
Creatures are destroyed if they fall off the below a 10 unit depth of the track height. When a creature is destroyed their fitness function is compared to the current best pool of weights and will replace them if distance > distance or distance = distance & time < time.

The pool of weights saved is the 3 highest fitness over all generations and the highest fitness from the previous generation.



The following round, creatures are spawned by either matching 1 of the 4 pools, or by pairing up 2 of the 4 pools and then randomly choosing between each weight between the two. Following this a random mutation % is applied to each weight.

