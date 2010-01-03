/*
* Box2D.XNA port of Box2D:
* Copyright (c) 2009 Brandon Furtwangler, Nathan Furtwangler
*
* Original source Box2D:
* Copyright (c) 2006-2009 Erin Catto http://www.gphysics.com 
* 
* This software is provided 'as-is', without any express or implied 
* warranty.  In no event will the authors be held liable for any damages 
* arising from the use of this software. 
* Permission is granted to anyone to use this software for any purpose, 
* including commercial applications, and to alter it and redistribute it 
* freely, subject to the following restrictions: 
* 1. The origin of this software must not be misrepresented; you must not 
* claim that you wrote the original software. If you use this software 
* in a product, an acknowledgment in the product documentation would be 
* appreciated but is not required. 
* 2. Altered source versions must be plainly marked as such, and must not be 
* misrepresented as being the original software. 
* 3. This notice may not be removed or altered from any source distribution. 
*/

using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FarseerPhysics.TestBed.Tests
{
    public class VerticalStackTest : Test
    {
        private const int ColumnCount = 5;
        private const int RowCount = 16;

        private VerticalStackTest()
        {
            {
                
                Body ground = _world.CreateBody();

                PolygonShape shape = new PolygonShape(0);
                shape.SetAsEdge(new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));
                ground.CreateFixture(shape);

                shape.SetAsEdge(new Vector2(20.0f, 0.0f), new Vector2(20.0f, 20.0f));
                ground.CreateFixture(shape);
            }

            float[] xs = new float[] { 0.0f, -10.0f, -5.0f, 5.0f, 10.0f };

            for (int j = 0; j < ColumnCount; ++j)
            {
                PolygonShape shape = new PolygonShape(1);
                shape.SetAsBox(0.5f, 0.5f);

                for (int i = 0; i < RowCount; ++i)
                {
                    

                    const float x = 0.0f;
                    //float x = Rand.RandomFloat-0.02f, 0.02f);
                    //float x = i % 2 == 0 ? -0.025f : 0.025f;
                    Body body = _world.CreateBody();
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(xs[j] + x, 0.752f + 1.54f * i);

                    Fixture fixture = body.CreateFixture(shape);
                    fixture.Friction = 0.3f;
                }
            }

            _bullet = null;
        }

        public override void Keyboard(KeyboardState state, KeyboardState oldState)
        {
            if (state.IsKeyDown(Keys.OemComma) && oldState.IsKeyUp(Keys.OemComma))
            {
                if (_bullet != null)
                {
                    _world.DestroyBody(_bullet);
                    _bullet = null;
                }

                {
                    CircleShape shape = new CircleShape(0.25f, 20);

                    _bullet = _world.CreateBody();
                    _bullet.BodyType = BodyType.Dynamic;
                    _bullet.Bullet = true;
                    _bullet.Position = new Vector2(-31.0f, 5.0f);

                    Fixture fixture = _bullet.CreateFixture(shape);
                    fixture.Restitution = 0.05f;

                    _bullet.LinearVelocity = new Vector2(400.0f, 0.0f);
                }
            }
        }

        public override void Step(Framework.Settings settings)
        {
            base.Step(settings);
            _debugView.DrawString(50, _textLine, "Press: (,) to launch a bullet.");
            _textLine += 15;
        }

        internal static Test Create()
        {
            return new VerticalStackTest();
        }

        private Body _bullet;
    }
}