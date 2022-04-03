```
Encrypts the message by the key. Example:
 input - too many secrets
   key - angel
   
The key is constantly repeated. Spaces and punctuation marks are not included:
   input - too many secrets
     key - ang elan gelange
  output - ucv rmom zjossax

Steps:
  a   b   c   d   e   f   g   h   i   j   k   l   m
  1   2   3   4   5   6   7   8   9  10  11  12  13
-25 -24 -23 -22 -21 -20 -19 -18 -17 -16 -15 -14 -13
  n   o   p   q   r   s   t   u   v   w   x   y   z
 14  15  16  17  18  19  20  21  22  23  24  25  26
-12 -11 -10  -9  -8  -7  -6  -5  -4  -3  -2  -1   0

  t + a = u (20 +  1 =  21)
  o + n = c (15 + 14 =  29; 29 - 26 = 3)
  ...
  s + e = x (19 +  5 =  24)
```