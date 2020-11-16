﻿
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AnimeTask.Sample
{
    public class Sample : MonoBehaviour
    {
        public async Task Sample01()
        {
            using (var cubes = new SampleCubes(new Vector3(-5f, 0f, 0f)))
            {
                await Anime.Delay(1f);
                await Anime.PlayTo(
                    Easing.Create<OutCubic>(new Vector3(5f, 0f, 0f), 2f),
                    TranslateTo.LocalPosition(cubes[0])
                );
                await Anime.Delay(1f);
            }
        }

        public async Task Sample02()
        {
            using (var cubes = new SampleCubes(new Vector3(0f, 3f, 0f), new Vector3(0f, 3f, 0f), new Vector3(0f, 3f, 0f)))
            {
                await Anime.Delay(1f);
                await Task.WhenAll(
                    CircleAnimation(cubes[0], 0.0f),
                    CircleAnimation(cubes[1], 0.2f),
                    CircleAnimation(cubes[2], 0.4f)
                );
                await Anime.Delay(1f);
            }
        }

        public async Task Sample03()
        {
            using (var cubes = new SampleCubes(new Vector3(-5f, 0f, 0f)))
            {
                await Anime.Delay(1f);
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Token.Register(() => Debug.Log("Cancel"));
                cancellationTokenSource.CancelAfter(500);

                await Anime.PlayTo(
                    Easing.Create<OutCubic>(new Vector3(5f, 0f, 0f), 2f),
                    TranslateTo.LocalPosition(cubes[0]),
                    cancellationTokenSource.Token
                );
                await Anime.Delay(1f);
            }
        }

        private async Task CircleAnimation(GameObject go, float delay)
        {
            await Anime.Delay(delay);
            await Anime.Play(
                Animator.Convert(Easing.Create<OutCubic>(0.0f, Mathf.PI * 2.0f, 2f),
                    x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f) * 3.0f),
                TranslateTo.LocalPosition(go)
            );
        }

        public async Task Sample04()
        {
            using (var cubes = new SampleCubes(new Vector3(-5f, 0f, 0f)))
            {
                await Anime.Delay(1f);
                await UniTask.WhenAll(
                    Anime.PlayTo(
                        Moving.Linear(2f, 2f),
                        TranslateTo.LocalPositionX(cubes[0])
                    ),
                    Anime.PlayTo(
                        Animator.Delay(1.8f, Easing.Create<Linear>(Vector2.zero, 0.2f)),
                        TranslateTo.LocalScale(cubes[0])
                    )
                );
                await Anime.Delay(1f);
            }
        }

        public async Task Sample05()
        {
            using (var cubes = new SampleCubes(new Vector3(-5f, 0f, 0f)))
            {
                await Anime.Delay(1f);
                await UniTask.WhenAll(
                    Anime.Play(
                        Easing.Create<OutCubic>(Quaternion.identity, Quaternion.Euler(30f, 0f, 0f), 0.5f),
                        TranslateTo.GlobalRotation(cubes[0])
                    )
                );
                await Anime.Delay(1f);
            }
        }
    }
}
