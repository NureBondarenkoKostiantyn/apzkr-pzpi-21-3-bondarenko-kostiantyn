-- Football
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Football', 'A team sport played with a spherical ball between two teams of eleven players.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Football')

-- Basketball
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Basketball', 'A team sport in which two teams, typically of five players each, compete to score points by throwing a ball through the opponent''s hoop.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Basketball')

-- Soccer
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Soccer', 'A team sport played with a spherical ball between two teams of eleven players.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Soccer')

-- Tennis
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Tennis', 'A racket sport that can be played individually against a single opponent (singles) or between two teams of two players each (doubles).'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Tennis')

-- Baseball
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Baseball', 'A bat-and-ball game played between two opposing teams who take turns batting and fielding.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Baseball')

-- Cricket
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Cricket', 'A bat-and-ball game played between two teams of eleven players on a field at the center of which is a 22-yard pitch with a wicket at each end.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Cricket')

-- Golf
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Golf', 'A club-and-ball sport in which players use various clubs to hit balls into a series of holes on a course in as few strokes as possible.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Golf')

-- Hockey
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Hockey', 'A sport played by two teams on ice or field, with sticks and a ball or puck, where players try to hit the ball or puck into the opposing team''s goal.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Hockey')

-- Volleyball
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Volleyball', 'A team sport in which two teams of six players are separated by a net. Each team tries to score points by grounding a ball on the other team''s court under organized rules.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Volleyball')

-- Swimming
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Swimming', 'A sport in which individuals or teams race in water, using various strokes and techniques to move through the water.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Swimming')

-- Athletics
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Athletics', 'A collection of sporting events that involve competitive running, jumping, throwing, and walking.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Athletics')

-- Cycling
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Cycling', 'A sport consisting of riding bicycles, unicycles, tricycles, or other human-powered vehicles.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Cycling')

-- Boxing
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Boxing', 'A combat sport in which two people, usually wearing protective gloves and other protective equipment such as hand wraps and mouthguards, throw punches at each other for a predetermined amount of time in a boxing ring.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Boxing')

-- Martial Arts
INSERT INTO [dbo].[Sports] ([Id], [Name], [Description])
SELECT NEWID(), 'Martial Arts', 'Various sports or skills, mainly of Japanese origin, that originated as forms of self-defense or attack, such as judo, karate, and kendo.'
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Sports] WHERE [Name] = 'Martial Arts')
