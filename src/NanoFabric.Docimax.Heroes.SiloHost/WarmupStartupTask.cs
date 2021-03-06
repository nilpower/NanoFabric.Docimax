﻿using NanoFabric.Docimax.Grains.Contracts.Heroes;
using Orleans;
using Orleans.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace NanoFabric.Docimax.Heroes.SiloHost
{
    public class WarmupStartupTask : IStartupTask
    {
        private readonly IGrainFactory _grainFactory;

        public WarmupStartupTask(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            //    var grain = _grainFactory.GetGrain<IHeroCollectionGrain>(0);
            //    await grain.Activate();

            //    var heroes = await grain.GetAll();
            //    foreach (var hero in heroes)
            //        _grainFactory.GetGrain<IHeroGrain>(hero.Key).InvokeOneWay(x => x.Activate());
        }
    }
}
